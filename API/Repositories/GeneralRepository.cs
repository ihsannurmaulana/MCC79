using API.Contracts;
using API.Data;

namespace API.Repositories;
public class GeneralRepository<TEnitity> : IGeneralRepository<TEnitity>
    where TEnitity : class
{
    protected readonly BookingDbContext _context;

    public GeneralRepository(BookingDbContext context)
    {
        _context = context;
    }

    public ICollection<TEnitity> GetAll()
    {
        return _context.Set<TEnitity>().ToList();
    }

    public TEnitity? GetByGuid(Guid guid)
    {
        return _context.Set<TEnitity>().Find(guid);
    }

    public TEnitity? Create(TEnitity enitity)
    {
        try
        {
            _context.Set<TEnitity>().Add(enitity);
            _context.SaveChanges();
            return enitity;
        }
        catch
        {
            return null;
        }
    }

    public bool Update(TEnitity enitity)
    {
        try
        {
            _context.Set<TEnitity>().Update(enitity);
            _context.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(Guid guid)
    {
        try
        {
            var entity = GetByGuid(guid);
            if (entity is null)
            {
                return false;
            }

            _context.Set<TEnitity>().Remove(entity);
            _context.SaveChanges();
            return true;

        }
        catch
        {
            return false;
        }
    }
}

