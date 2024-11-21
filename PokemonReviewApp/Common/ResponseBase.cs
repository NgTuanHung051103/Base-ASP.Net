namespace PokemonReviewApp.Common.DataGram
{
    public class ResponseBase
    {
        private ResponseStatus _status;
        private bool _success;
        private string? _message;

        private readonly Dictionary<string, object?> _tempData;
        public ResponseBase() 
        {
            _tempData = new Dictionary<string, object?>();
            Data = new Dictionary<string, object?>();
            Success = false;
        }
        
        public Dictionary<string, object?> Data { get; set; }
        
        public ResponseStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                Data["status"] = (int)value;
            }
        }

        public string? Message
        {
            get { return _message; }
            set
            {
                _message = value;
                Data["message"] = value;
            }
        }


        public bool Success
        {
            get { return _success; }
            set
            {
                _success = value;
                Data["success"] = value; 
            }
        }
        public void AddTempData(string keyName, object? data)
        {
            _tempData.TryAdd(keyName, data);
            Data["data"] = _tempData;
        }
    }
}
