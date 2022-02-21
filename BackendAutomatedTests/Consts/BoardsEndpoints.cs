namespace BackendAutomatedTests.Consts
{
    public class BoardsEndpoints
    {
        public const string GetAllBoardsUrl = "/1/members/{member}/boards";
        public const string GetBoardUrl = "/1/boards/{id}";
        public const string GetAllListsOnBoardUrl = "/1/boards/{id}/lists";

        public const string CreateBoardUrl = "/1/boards";
        public const string DeleteBoardUrl = "/1/boards/{id}";
        public const string UpdateBoardUrl = "/1/boards/{id}";
    }
}
