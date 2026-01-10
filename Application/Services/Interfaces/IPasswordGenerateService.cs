namespace Application.Services.Interfaces;

public interface IPasswordGenerateService
{
    public string Generate(int length);
}