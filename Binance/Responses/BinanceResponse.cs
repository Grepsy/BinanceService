namespace Binance.Responses {
    // TODO: Remove?
    public class BinanceResponse<T> {
        public T Content { get; set; }
        public BinanceErrorType ErrorType { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }

        internal BinanceResponse(T content) {

        }

        public enum BinanceErrorType {
            Malformed,
            InternalError,
            InternalTimeout,
            Unknown
        }
    }
}
