using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bradsol.BattleShip.UI
{
    public static class ExceptionHelper
    {
        public static string GetAllExceptionMessages(this Exception ex)
        {
            string messages = string.Join(Environment.NewLine, ex.GetInnerExceptions().ToList().Select(x => x.Message));

            return messages;
        }

        private static IEnumerable<Exception> GetInnerExceptions(this Exception ex)
        {
            if (ex == null)
            {
                throw new ArgumentNullException("ex");
            }

            var innerException = ex;
            do
            {
                yield return innerException;
                innerException = innerException.InnerException;
            }
            while (innerException != null);
        }
    }
}
