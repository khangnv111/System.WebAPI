namespace System.WebAPI.Constants;

public class GroupControllerRoute
{
    public const string GetGroup = ControllerName.Groups + nameof(GetGroup);
    public const string AddGroup = ControllerName.Groups + nameof(AddGroup);
    public const string UpdateGroup = ControllerName.Groups + nameof(UpdateGroup);
    public const string ConfigPermission = ControllerName.Groups + nameof(ConfigPermission);
}
