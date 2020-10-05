using System.Collections.Generic;

namespace Cats
{
	public class CatData
	{
		public IEnumerable<Cat> Cats { get; set; }
		public IEnumerable<Owner> Owners { get; set; }
		public IEnumerable<OwnerCat> OwnerCats { get; set; }
	}


	public class Cat
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IList<OwnerCat> OwnerCats { get; set; } = new List<OwnerCat>();
	}

	public class Owner
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public IList<OwnerCat> OwnerCats { get; set; } = new List<OwnerCat>();
	}

	public class OwnerCat
	{
		public int CatId { get; set; }
		public Cat Cat { get; set; }

		public int OwnerId { get; set; }
		public Owner Owner { get; set; }

		public static OwnerCat CreateFrom(Cat cat, Owner owner)
		{
			var ownerCat = new OwnerCat { Cat = cat, CatId = cat.Id, Owner = owner, OwnerId = owner.Id };
			cat.OwnerCats.Add(ownerCat);
			owner.OwnerCats.Add(ownerCat);
			return ownerCat;
		}
	}

}
