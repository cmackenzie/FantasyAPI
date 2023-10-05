using FantasyCore.Models;
using System.Security.Cryptography;
using System.Text;

namespace FantasyProcessor.Utils
{
	public class PlayerChecksumHelper
	{
		public static string GenerateChecksum(Player player)
		{
            string rawString = new StringBuilder()
                .AppendFormat("firstName_{0}", player.FirstName)
                .AppendFormat("lastname_{0}", player.LastName)
                .AppendFormat("fullname_{0}", player.FullName)
                .AppendFormat("age_{0}", player.Age)
                .AppendFormat("jersey_{0}", player.JerseyNumber)
                .AppendFormat("position_{0}", player.PositionId)
                .AppendFormat("team_{0}", player.TeamId)
                .ToString();

			MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(rawString);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }
	}
}

