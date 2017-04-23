using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GenerationScript : MonoBehaviour {


    public int tau = 12;
    public int upper_bound_i = 16;
    public int upper_bound_j = 16;
    public float SphereSize = 1;
    public float SphereSpacing = 1;
    public GameObject prefab;
    public GameObject parent;


    private int i;
    private int j;
    private int t;
    private int thyme;
    private int k1;
    private int k2;
    private int upper_bound_T = 10;
    private int lower_bound_i = 1;
    private int lower_bound_j = 1;
    private ArrayList data = new ArrayList();
    private GameObject obj;
    private Color col;

    // Use this for initialization
    void Start () {
        generateData();
        int n = 0;
        col = Color.white;
        for(int i=0; data.Count > i; i++) {
            if (data[i].GetType()==typeof(String)) {
                switch (n) {
                    case (0):
                        col = Color.black;
                        break;
                    case (1):
              
                        col = Color.white;
                        break;
                    case (2):
                        
                        col = Color.red;
                        break;

                    case (3):
                        col = Color.blue;
                        break;

                    case (4):
                        col = Color.magenta;
                        break;

                    case (5):
                        col = Color.green;
                        break;

                    case (6):
                        col = Color.yellow;
                        break;

                    case (7):
                        col = Color.cyan;
                        break;
                }

                if (n == 7) {
                    n = 0;
                } else {
                    n++;
                }

            } else {
                obj = Instantiate(prefab);
                obj.transform.parent = parent.transform;
                obj.transform.localScale = new Vector3(SphereSize,SphereSize,SphereSize);
                obj.transform.position = (Vector3)data[i];
                obj.GetComponent<MeshRenderer>().material.SetColor("_Color",col);
            }
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private double intDiv(double i, double j) {
        if ((i) % (j) >= 0) {
            return i / j;
        } else {
            return ((i) / (j)) - 1;
        }
    }

    private double intMod(double i, double j) {
        if ((i) % (j) >= 0) {
            return i % j;
        } else {
            return ((i) % (j)) + j;
        }

    }
    private double ceild(double n, double d) {
        if (intMod(n, d) > 0) {
            return intDiv(n, d) + 1;
        } else {
            return intDiv(n, d);
        }
    }

    private void generateData() {

        for (thyme = (int)ceild(3, tau) - 3; thyme <= intDiv(3 * upper_bound_T, tau); thyme++) {
            // The next two loops iterate within a tile wavefront.
            int k1_lb = (int)ceild((double)(3 * lower_bound_j + 2 + (thyme - 2) * tau), (double)(tau * 3));
            int k1_ub = (int)intDiv((double)(3 * upper_bound_j + (thyme + 2) * tau), (double)(tau * 3));

            int k2_lb = (int)intDiv((2 * thyme - 2) * tau - 3 * upper_bound_i + 2, tau * 3);
            int k2_ub = (int)intDiv((2 + 2 * thyme) * tau - 2 - 3 * lower_bound_i, tau * 3);

            for (int x = k2_lb; x <= k2_ub; x++) {
                for (k1 = k1_lb; k1 <= k1_ub; k1++) {
                    k2 = x - k1;

                    // Loop over time within a tile.
                    for (t = (int)Math.Max(1, intDiv(thyme * tau - 1, 3) + 1); t < Math.Min(upper_bound_T + 1,
                            tau + intDiv(thyme * tau, 3)); t++) {
                        // Loops over spatial dimensions within tile.
                        for (i = Math.Max(lower_bound_i,
                                Math.Max(-2 * tau - k1 * tau - k2 * tau + 2 * t + 2,
                                        (thyme - k1 - k2) * tau - t)); i <= Math.Min(upper_bound_i,
                                                Math.Min(tau + (thyme - k1 - k2) * tau - t - 1,
                                                        -k1 * tau - k2 * tau + 2 * t)); i += 1) {
                            for (j = Math.Max(lower_bound_j,
                                    Math.Max(k1 * tau - t, -tau - k2 * tau + t - i + 1)); j <= Math.Min(upper_bound_j,
                                            Math.Min(tau + k1 * tau - t - 1, -k2 * tau + t - i)); j += 1) {
                                // this.data += (t + "," + i + "," + j + "\n");
                                data.Add(new Vector3(t*SphereSpacing, i*SphereSpacing, j*SphereSpacing));
                            } // for j
                        } // for i
                    } // for t
                } // for k2
                //this.data += ("~\n");
                data.Add("~");
            } // for k1
              //Add color change to string
            
        } // for thyme
    }




}
