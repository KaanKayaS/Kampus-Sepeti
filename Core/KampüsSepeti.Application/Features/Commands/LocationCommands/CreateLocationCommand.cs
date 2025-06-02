using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KampüsSepeti.Application.Features.Commands.LocationCommands
{
    public class CreateLocationCommand : IRequest<Unit>
    {
        public string Name { get; set; }
    }
}





















// Normalde bu işlemde bir response dönmek zorunda değiliz ama fluent beheviorun düzgün çalışması Boş bir değer olan Uniti dönüyoruz 
// çünkü orda kullanıcıdan alınan requestte pipelinede bir response değeri dönüyor ondan dolayı bizim kod patlıyor