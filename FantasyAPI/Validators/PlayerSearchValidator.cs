using FantasyCore;
using FantasyAPI.Models.Request;

namespace FantasyAPI.Validators
{
    /// <summary>
    /// Class to validate the search input
    /// In more robust projects this is likely a custom attribute
    /// that inspects the request before it hits the method body
    /// and then appropriately sets the status code w/ a failure response
    /// However, in this project it simply errors out with the below errors
    /// </summary>
	public class PlayerSearchValidator
	{
		public static void Validate(int offset, int take, PlayerSearch? playerSearch)
		{
			if(offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset must be greater than 0");
			}

            if (take <= 0 || take > 100)
            {
                throw new ArgumentOutOfRangeException("take must be greater than 0 and less than 100");
            }

            if(playerSearch == null)
            {
                return;
            }

			if(playerSearch.age != null)
			{
				if(playerSearch.age.Length == 0)
				{
                    throw new ArgumentOutOfRangeException("you must provide at least one value for age");
                }

                if (playerSearch.age.Length == 2)
                {
                    if (playerSearch.age[0] > playerSearch.age[1])
                    {
                        throw new ArgumentOutOfRangeException("your start age must be less than or equal to you end age");
                    }
                }
                else if (playerSearch.age.Length > 2)
                {
                    throw new ArgumentOutOfRangeException("you may only provide up to 2 values for age range");
                }
            }

            if(playerSearch.last_name_prefix != null && playerSearch.last_name_prefix.Length > 1000)
            {
                throw new ArgumentOutOfRangeException("last_name_prefix is longer than 1000 characters.");
            }

            if (playerSearch.position != null && playerSearch.position.Length > 100)
            {
                throw new ArgumentOutOfRangeException("position is longer than 100 characters.");
            }

            if (playerSearch.sport != null)
            {
                if (playerSearch.sport != Constants.BASEBALL &&
                    playerSearch.sport != Constants.BASKETBALL &&
                    playerSearch.sport != Constants.FOOTBALL)
                {
                    throw new ArgumentOutOfRangeException("sport is not supported.");
                }
            }
        }
	}
}

