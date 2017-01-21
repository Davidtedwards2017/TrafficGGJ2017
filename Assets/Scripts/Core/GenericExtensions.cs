using UnityEngine;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;

public static class GenericExtensions {

	public static void AddIfUnique<T>(this List<T> collection, T entry)
    {
        if(entry != null && !collection.Contains(entry))
        {
            collection.Add(entry);
        }
    }

    public static void SafeAdd<T>(this List<T> collection, T value)
    {
        if (!collection.Contains(value))
        {
            collection.Add(value);
        }
    }

    public static void SafeRemove<T>(this List<T> collection, T value)
    {
        if (collection.Contains(value))
        {
            collection.Remove(value);
        }
    }

    public static void SafeRemove<T, K>(this Dictionary<T, K> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary.Remove(key);
        }
    }

    public static K SafeGetOrInitialize<T, K>(this Dictionary<T, K> dictionary, T key) where K: new()
    {
        if (dictionary.ContainsKey(key))
        {
            return (K)dictionary[key];
        }
        else
        {
            var instance = new K();
            dictionary.Add(key, instance);
            return instance;
        }
    }

    public static void Shuffle<T>(this IList<T> list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static void AddOrUpdate<T, K>(this Dictionary<T, K> dictionary, T key, K value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    public static T[] FlipRows<T>(this T[] grid, int numRows, int numColumns)
    {
        var tempMatrix = new T[grid.Length];
        int index = 0;
        for (int r = numRows - 1; r >= 0; r--)
        {
            for (int c = 0; c < numColumns; c++)
            {
                tempMatrix[index++] = grid[(r * numColumns) + c];
            }
        }

        return tempMatrix;
    }

    public static T[] FlipColumns<T>(this T[] grid, int numRows, int numColumns)
    {
        var tempMatrix = new T[grid.Length];
        int index = 0;
        for (int r = 0 ; r < numRows; r++)
        {
            for (int c = numColumns - 1; c >= 0; c--)
            {
                tempMatrix[index++] = grid[(r * numColumns) + c];
            }
        }

        return tempMatrix;
    }

    public static T[] SwapRowsAndColumns<T>(this T[] grid, ref int numRows, ref int numColumns)
    {
        int newRows = numColumns;
        int newCols = numRows;

        numRows = newRows;
        numColumns = newCols;

        var tempMatrix = new T[grid.Length];
        int index = 0;
        for (int r = 0; r < numRows; r++)
        {
            for (int c = numColumns - 1; c >= 0; c--)
            {
                tempMatrix[index++] = grid[(c * numRows) + r];
            }
        }

        return tempMatrix;
    }
}