using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Exploria.CodeChallenge.Weather.Application.Feature.Meteorology;

public record  MeteorologyInquiryRequestDto(): IRequest<MeteorologyInquiryResponseDto>;

