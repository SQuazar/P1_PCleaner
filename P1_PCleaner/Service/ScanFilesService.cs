using P1_PCleaner.Repository;

namespace P1_PCleaner.Service;

public class ScanFilesService : IScanFilesService
{

    private readonly IFilesRepository _filesRepository;

    public ScanFilesService(IFilesRepository repository)
    {
        _filesRepository = repository;
    }
    
    public bool Scan()
    {
        return false;
    }
}