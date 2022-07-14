namespace System.WebAPI.Mappers;

using System.Models.Model.Admin;
using System.Models.ViewModel.User;
using Boxed.Mapping;

public class GroupMapper : IMapper<SaveGroup, Group>
{
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly LinkGenerator linkGenerator;

    public GroupMapper(
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        this.httpContextAccessor = httpContextAccessor;
        this.linkGenerator = linkGenerator;
    }

    public void Map(SaveGroup source, Group destination)
    {
        //ArgumentNullException.ThrowIfNull(source);
        //ArgumentNullException.ThrowIfNull(destination);

        destination.GroupName = source.GroupName;
        destination.Description = source.Description;
    }
}
