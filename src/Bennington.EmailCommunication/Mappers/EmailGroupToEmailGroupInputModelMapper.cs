﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapperAssist;
using Bennington.EmailCommunication.Models;

namespace Bennington.EmailCommunication.Mappers
{
    public interface IEmailGroupToEmailGroupInputModelMapper
    {
        EmailGroupInputModel CreateInstance(EmailGroup source);
    }

    public class EmailGroupToEmailGroupInputModelMapper : Mapper<EmailGroup, EmailGroupInputModel>, IEmailGroupToEmailGroupInputModelMapper
    {
    }
}