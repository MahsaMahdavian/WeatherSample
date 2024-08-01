using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Application.Provider.Meteorology;

public interface IMeteorologyInquiryProvider
{
   Task<MeteorologyInquiryResponse> GetWatherInfo(CancellationToken cancellationToken);
}
