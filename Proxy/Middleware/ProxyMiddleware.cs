using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Proxy.Middleware
{
    public class ProxyMiddleware
    {
		private static readonly HttpClient _httpClient = new HttpClient();
		private readonly RequestDelegate _nextMiddleware;

		public ProxyMiddleware(RequestDelegate nextMiddleware)
		{
			_nextMiddleware = nextMiddleware;
		}

		public async Task Invoke(HttpContext context)
		{
				// If no requests found in cache - forward to the server
				var targetUri = BuildTargetUri(context.Request);

				if (targetUri != null)
				{
					var targetRequestMessage = CreateTargetMessage(context, targetUri);

					using (var responseMessage = await _httpClient.SendAsync(targetRequestMessage, HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
					{
						context.Response.StatusCode = (int)responseMessage.StatusCode;

						CopyFromTargetResponseHeaders(context, responseMessage);

						await ProcessResponseContent(context, responseMessage);
					}

					return;
				}

				await _nextMiddleware(context);
			
		}


		private async Task ProcessResponseContent(HttpContext context, HttpResponseMessage responseMessage)
		{
			var content = await responseMessage.Content.ReadAsByteArrayAsync();

			await context.Response.Body.WriteAsync(content);
		}


		private HttpRequestMessage CreateTargetMessage(HttpContext context, Uri targetUri)
		{
			var requestMessage = new HttpRequestMessage();
			CopyFromOriginalRequestContentAndHeaders(context, requestMessage);

			requestMessage.RequestUri = targetUri;
			requestMessage.Headers.Host = targetUri.Host;
			requestMessage.Method = GetMethod(context.Request.Method);

			return requestMessage;
		}

		private void CopyFromOriginalRequestContentAndHeaders(HttpContext context, HttpRequestMessage requestMessage)
		{
			var requestMethod = context.Request.Method;

			if (!HttpMethods.IsGet(requestMethod))
			{
				var streamContent = new StreamContent(context.Request.Body);
				requestMessage.Content = streamContent;
			}

			foreach (var header in context.Request.Headers)
			{
				if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
				{
					requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
				}
			}

			/*foreach (var header in context.Request.Headers)
			{
				//requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
				requestMessage.Content.Headers.Add(header.Key, header.Value.ToArray());
			}*/
		}

		private void CopyFromTargetResponseHeaders(HttpContext context, HttpResponseMessage responseMessage)
		{
			foreach (var header in responseMessage.Headers)
			{
				context.Response.Headers[header.Key] = header.Value.ToArray();
			}

			foreach (var header in responseMessage.Content.Headers)
			{
				context.Response.Headers[header.Key] = header.Value.ToArray();
			}
			context.Response.Headers.Remove("transfer-encoding");
		}

		private static HttpMethod GetMethod(string method)
		{
			if (HttpMethods.IsDelete(method)) return HttpMethod.Delete;
			if (HttpMethods.IsGet(method)) return HttpMethod.Get;
			if (HttpMethods.IsPost(method)) return HttpMethod.Post;
			if (HttpMethods.IsPut(method)) return HttpMethod.Put;

			return new HttpMethod(method);
			//throw new Exception
		}

		// In case it's a call to the API - get the least loaded server address
		// and create a URL for it's forwarding
		private Uri BuildTargetUri(HttpRequest request)
		{
			Uri targetUri = null;
			PathString remainingPath;

			if (request.Path.StartsWithSegments("/api", out remainingPath))
			{
				targetUri = new Uri("http://localhost:64088/api" + remainingPath);
			}

			return targetUri;
		}

		
	}
}

