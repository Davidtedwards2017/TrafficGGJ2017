using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WeightedList<T> {

	private List<T> elements;
	private List<int> weights;
	
	private int _length = 0;
	public int Length
	{
		get { return _length; }
	}
	public int Count
	{
		get { return _length; }
	}
	
	public WeightedList ()
	{
		elements = new List<T>();
		weights = new List<int>();
	}
	
	public void Add (T element, int weight)
	{
		elements.Add(element);
		weights.Add(weight);
		_length++;
	}
	public void Add (int weight, T element) { Add(element, weight); }
	
	public void Remove (T element)
	{
		weights.RemoveAt(elements.IndexOf(element));
		elements.Remove(element);
		_length--;
	}
	
	public void RemoveAt (int i)
	{
		weights.RemoveAt(i);
		elements.RemoveAt(i);
		_length--;
	}
	
	public void Clear ()
	{
		weights.Clear();
		elements.Clear();
		_length = 0;
	}
	
	public T RandomElement()
	{
		int[] cumulativeMass = weights.ToArray();
		for (int i = 1; i < cumulativeMass.Length; i++)
		{
			cumulativeMass[i] += cumulativeMass[i-1];
		}
		int total = cumulativeMass[cumulativeMass.Length-1];
		for (int i = 0; i < cumulativeMass.Length; i++)
		{
			if (Random.value * total <= cumulativeMass[i])
			{
				return elements[i];
			}
		}
		return elements[0];
	}
}
