using System.ComponentModel.DataAnnotations;

namespace TestTask.Minesweeper.Service.Api
{
	/// <summary>
	/// Represents a type of field.
	/// </summary>
	public enum FieldType
	{
		/// <summary>
		/// Empty or unknown field.
		/// </summary>
		[Display(Name = " ")]
		Empty,

		/// <summary>
		/// Field with one mine near.
		/// </summary>
		[Display(Name = "1")]
		One,

		/// <summary>
		/// Field with two mines near.
		/// </summary>
		[Display(Name = "2")]
		Two,

		/// <summary>
		/// Field with three mines near.
		/// </summary>
		[Display(Name = "3")]
		Three,

		/// <summary>
		/// Field with four mines near.
		/// </summary>
		[Display(Name = "4")]
		Four,

		/// <summary>
		/// Field with five mines near.
		/// </summary>
		[Display(Name = "5")]
		Five,

		/// <summary>
		/// Field with six mines near.
		/// </summary>
		[Display(Name = "6")]
		Six,

		/// <summary>
		/// Field with seven mines near.
		/// </summary>
		[Display(Name = "7")]
		Seven,

		/// <summary>
		/// Field with eight mines near.
		/// </summary>
		[Display(Name = "8")]
		Eight,

		/// <summary>
		/// Field with mine.
		/// </summary>
		[Display(Name = "M")]
		Mine,

		/// <summary>
		/// Field with exploded mine.
		/// </summary>
		[Display(Name = "X")]
		MineExploded
	}
}
