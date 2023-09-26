// <copyright file="EmailInput.cs" company="Mesh Systems LLC">
// Copyright © 2023 Mesh Systems LLC.
// </copyright>

using Mesh.Shared.Notifications;
using System.Collections.Generic;

namespace AbpCompanyName.AbpProjectName.Notifications.Email;

public class EmailInput
{
    public List<string> Addresses { get; set; } = new List<string>();

    public string Subject { get; set; }

    public string Body { get; set; }

    public bool IsHtml { get; set; } = true;

    public List<Attachment> Attachments { get; set; } = new List<Attachment>();
}
