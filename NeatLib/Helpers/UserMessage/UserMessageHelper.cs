using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace NeatLib.Helpers.UserMessage
{
    public static class UserMessageHelper
    {
        public static void AddUserMessage(HttpContext context,string title, UserMessageType type, string message) {
            IList<UserMessage> messages;
            if (!context.Items.ContainsKey("UserMessage"))
            {
                messages = new List<UserMessage>();
            }
            else
            {
                messages = (List<UserMessage>)context.Items["UserMessage"];
            }
            messages.Add(new UserMessage() {
                Message = message,
                Title = title,
                Type = type
            });
        }
        public static void AddUserMessageInformation(HttpContext context, string title, string message)
        {
            AddUserMessage(context, title, UserMessageType.Information, message);
        }
        public static void AddUserMessageSuccess(HttpContext context, string title, string message)
        {
            AddUserMessage(context, title, UserMessageType.Success, message);
        }
        public static void AddUserMessageWarning(HttpContext context, string title, string message)
        {
            AddUserMessage(context,title, UserMessageType.Warning, message);
        }
        public static void AddUserMessageError(HttpContext context, string title, string message)
        {
            AddUserMessage(context, title, UserMessageType.Error, message);
        }
    }
}
