namespace ControlWorks.Services.PVI
{
    public class VariableDetailRespose
    {
        public string CpuName { get; set; }
        public string[] VariableNames { get; set; }
        public ErrorResponse Errors { get; set; }
    }
}
