using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MeshConstruction {

	public static class MeshUtility {
		
	}

	public class Quad {
		public Triangle t1;
		public Triangle t2;

		public Quad ( Vector3 a, Vector3 b, Vector3 c, Vector3 d ) {
			t1 = new Triangle ( a, b, c );
			t2 = new Triangle ( a, c, d );
		}

		public Quad (Triangle t1, Triangle t2) {
			this.t1 = t1;
			this.t2 = t2;
		}
	}

	public class Triangle {
		public Vector3 a;
		public Vector3 b;
		public Vector3 c;

		public Triangle ( Vector3 a, Vector3 b, Vector3 c ) {
			this.a = a;
			this.b = b;
			this.c = c;
		}
	}
}