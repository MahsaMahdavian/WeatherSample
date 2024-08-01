using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Application.Exceptions;

public class BusinessException:Exception
{
    public int Code { get; private set; }

    public string Title { get; private set; }

    public BusinessException(int code, string title)
    {
        Code = code;
        Title = title;
    }

    public BusinessException(int code, string title, string message)
        : base(message)
    {
        Code = code;
        Title = title;
    }
}
