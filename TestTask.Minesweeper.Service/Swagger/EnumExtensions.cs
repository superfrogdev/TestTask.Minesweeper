using System.ComponentModel.DataAnnotations;

namespace TestTask.Minesweeper.Service.Swagger
{
	/// <summary>
	/// Represents extensions for <see cref="Enum"/>.
	/// </summary>
	internal static class EnumExtensions
	{
		/// <summary>
		/// Gets if exists attribute with specified <typeparamref name="T"/> from <paramref name="enumValue"/>.
		/// </summary>
		/// <typeparam name="T">Type of <see cref="Attribute"/>.</typeparam>
		/// <param name="enumValue">Value of <see cref="Enum"/>.</param>
		/// <returns>Instance of <typeparamref name="T"/> or <see langword="null"/>.</returns>
		public static T? GetAttributeOfType<T>(this Enum enumValue)
			where T : Attribute
		{
			var type = enumValue.GetType();

			var memberInfo = type.GetMember(enumValue.ToString());

			var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

			return (attributes.Length > 0) ? (T)attributes[0] : null;
		}

		/// <summary>
		/// Gets value of <see cref="DisplayAttribute"/> from <paramref name="enumValue"/> or <see cref="string"/> representation of <paramref name="enumValue"/>.
		/// </summary>
		/// <param name="enumValue">Value of <see cref="Enum"/>.</param>
		/// <returns>Value of <see cref="DisplayAttribute"/> from <paramref name="enumValue"/> or <see cref="string"/> representation of <paramref name="enumValue"/></returns>
		public static string GetDisplayName(this Enum enumValue)
		{
			var displayAttribute = GetAttributeOfType<DisplayAttribute>(enumValue);

			if (displayAttribute != null
					&& displayAttribute.Name != null)
			{
				return displayAttribute.Name;
			}
			else
			{
				return enumValue.ToString();
			}
		}
	}
}
