using System;
namespace German.Core.Interfaces
{
	public interface IAuditableEntity
	{
		 int Id { get; set; }
		 DateTime DateCreated { get; set; }
         DateTime DateUpdated { get; set; }
		 DateTime? DateDeleted { get; set; }
		 bool IsDeleted { get; set; }


    }
}

