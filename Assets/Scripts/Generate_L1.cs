using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Generate_L1 : MonoBehaviour {
	public class Vector16 {
		public List<int> get;
		public Vector16(int a, int b, int c, int d, int e, int f, int g,
		                int h, int i, int j, int k, int l,  int m, int n,
		                int o, int p) {
			get = new List<int>();
			get.Add (a);
			get.Add (b);
			get.Add (c);
			get.Add (d);
			get.Add (e);
			get.Add (f);
			get.Add (g);
			get.Add (h);
			get.Add (i);
			get.Add (j);
			get.Add (k);
			get.Add (l);
			get.Add (m);
			get.Add (n);
			get.Add (o);
			get.Add (p);
		}
	}
	
	public List<Vector16> world;
	public List<GameObject> worldBlocks;
	
	void Awake() {
		world = new List<Vector16>();
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1));
		world.Add(new Vector16(1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1));
		world.Add(new Vector16(1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1));
		world.Add(new Vector16(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1));
		world.Add(new Vector16(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1));
		world.Add(new Vector16(1, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 8, 0, 0,11, 1));
		world.Add(new Vector16(1, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
		world.Add(new Vector16(1, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1));
		world.Add(new Vector16(1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 0, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 1, 1, 1, 1, 1, 1, 1, 7, 7, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(1, 1, 1, 1, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 6, 6, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 1, 1, 1, 1));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(1, 1, 1, 1, 2, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 6, 6, 6, 6, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0));
		world.Add(new Vector16(0, 0, 8, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 1, 0, 0));
		world.Add(new Vector16(0, 0, 2, 6, 6, 2, 0, 0, 2, 0, 0, 2, 0, 1, 0, 0));
		world.Add(new Vector16(0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0));
		world.Add(new Vector16(6, 6, 2, 0, 0, 2, 0, 0, 2, 0, 0, 2, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 8, 0, 0));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 6, 6, 6, 6, 0, 6, 6, 6, 6, 6));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 4, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 0, 3));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 4, 4, 4, 4, 2, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 3, 3, 3, 3, 3, 3, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 6, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 0, 6, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 2, 2));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 6, 0, 2, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 0, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 6, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 6, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 6, 2, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 6, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 3, 3));
		world.Add(new Vector16(2, 0, 0, 2, 0, 0, 0, 2, 3, 3, 3, 3, 3, 3, 3, 3));
		world.Add(new Vector16(0, 0, 6, 2, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 2, 0, 8, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 2, 0, 6, 6, 2, 2, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 2, 2, 0, 0, 0, 6, 6, 2, 2));
		world.Add(new Vector16(2, 2, 2, 2, 6, 6, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 2, 2));
		//above here is mixed floor types
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 6, 6, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 6, 6, 6, 6, 6, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 6, 6, 6, 6, 0, 0, 6, 6, 6, 6, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 6, 6, 0, 0, 6, 6, 6, 6, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 6, 6, 0, 0, 6, 6, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 6, 0, 0, 0, 6, 6, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 6, 6, 6, 6, 6, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 6, 6, 6, 6, 0, 0, 6, 6, 6, 6, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 6, 6, 0, 0, 6, 6, 6, 6, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 6, 6, 0, 0, 6, 6, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		//above here is thin platforms		
		world.Add(new Vector16(0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 2, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 2, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 2, 4, 4, 4, 4, 0, 0, 0));
		world.Add(new Vector16(0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0));
		world.Add(new Vector16(4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 2, 4, 4, 4, 2));
		world.Add(new Vector16(0, 0, 2, 2, 8, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 2, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 0, 0, 0, 2, 2, 0, 7, 7, 0, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 0, 0, 0, 2, 4, 4, 4, 4, 0, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 9, 2, 2, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 2, 2, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 2, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 4, 4, 4, 4, 2, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 4, 4, 4, 4, 2, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 2));
		world.Add(new Vector16(2, 2, 2, 2, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 0));
		world.Add(new Vector16(0, 0, 0, 2, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
		world.Add(new Vector16(2, 2, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2));
		world.Add(new Vector16(2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 4, 4, 4, 4, 2, 2, 0, 7, 7, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 2, 0, 0, 0, 0, 2, 4, 4, 4, 4, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 2));
		world.Add(new Vector16(2, 4, 4, 4, 4, 0, 0, 0, 0, 0, 0, 0, 2, 2, 2, 2));
		world.Add(new Vector16(2, 2, 2, 2, 4, 4, 4, 4, 4, 4, 4, 4, 2, 2, 2, 2));
		world.Add(new Vector16(2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2));
	}
	
	void Start() {
		int rows = world.Count;
		for (int y = 0; y < rows; ++y) {
			for (int x = 0; x < 16; ++x) {
				Build(world[y].get[x], x, rows - y);
			}
			Build(world[y].get[0], 16, rows - y);
			Build(world[y].get[15], -1, rows - y);
		}
	}
	
	void Build(int ID, int x, int y) {
		if (--ID == -1) {
			return;
		}
		GameObject newObject = Instantiate (worldBlocks[ID]) as GameObject;
		newObject.transform.position = new Vector3(x - 8, y, 0);
		newObject.transform.parent = transform;
	}
}
