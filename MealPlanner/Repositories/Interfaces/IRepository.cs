﻿using System;
namespace MealPlanner.Repositories.Interfaces
{
	public interface IRepository<T>
	{
		Task<int> Count();
		Task<IEnumerable<T>> GetItems();
		Task<IEnumerable<T>> GetItems(Func<T, bool> filter);
		Task<T?> GetItemByUniqueValue(Func<T, bool> filter);
		Task<T> GetTopItemFromSort(Func<T, T, int> filter, bool getBottom = false);
		Task<T> SaveItem(T itemToSave);
		Task<T> UpdateItem(T updatedItem);
	}
}

