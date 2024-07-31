using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Domain.Entities;

public class ResultCode
{
    public int Code { get; private set; } = 3500;
    public string Value { get; private set; } = "Failure";
    public string Description { get; private set; } = "عملیات با شکست مواجه شد";

    private ResultCode()
    {

    }

    private ResultCode(int code,
        string value,
        string description)
    {
        Code = code;
        Value = value;
        Description = description;
    }
    public static ResultCode ProviderResponseError => new(5517, nameof(ProviderResponseError), "عدم امکان سرویس دهی");
    public static ResultCode Failure => new(3500, nameof(Failure), "عملیات با شکست مواجه شد");
}
