using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto;
public class FileMediaResponseDto
{
    public int StatusCode { get; set; }
    public List<string> Message { get; set; } = null!;
    public int Total { get; set; } = 0;
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 10;
    public List<FuelStation.PartExchange.Application.DTOs.FileMediaDto>? Result { get; set; }
}
