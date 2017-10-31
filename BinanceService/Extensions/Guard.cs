using System;
using System.Collections.Generic;
using System.Text;

namespace BinanceService.Extensions {
    public static class Guard {
        public static void ArgumentNotNull(object value, string argument) {
            if (value == null) {
                throw new ArgumentNullException(argument);
            }
        }

        public static void ArgumentNotNullOrWhitespace(string value, string argument) {
            if (String.IsNullOrWhiteSpace(value)) {
                throw new ArgumentNullException(argument);
            }
        }
    }
}
