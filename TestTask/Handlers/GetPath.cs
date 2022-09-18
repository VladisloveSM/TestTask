namespace TestTask.Handlers
{
    static class GetPath
    {
        public static string GetJsonPath(int number) => $@"..\..\TestData\day{number}.json";
    }
}
