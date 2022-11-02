using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    
    [SerializeField] GameObject carPrefab;
    [SerializeField] private float moveSpeed;
    [SerializeField] private List<GameObject> leftCars = new List<GameObject>();
    [SerializeField] private List<GameObject> righttCars = new List<GameObject>();
    [SerializeField] private List<Transform> leftCarsTransform = new List<Transform>();
    [SerializeField] private List<Transform> rightCarsTransform = new List<Transform>();
    private int gateNumber;
    private int leftTargetCount;
    private int rightTargetCount;

    Sequence mySeq;

    int leftindexOfTransforms = 0;
    int rightindexOfTransforms = 0;

    public GameObject blueTruck;
    public GameObject redTruck;
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        this.transform.Translate(horizontal, 0 , moveSpeed * Time.deltaTime);
        SwapCars();
    }


	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.CompareTag("Gate"))
		{
            
            gateNumber = other.gameObject.GetComponent<GateController>().GetGateNumber();
            leftTargetCount = leftCars.Count + gateNumber;
            rightTargetCount = righttCars.Count + gateNumber;
			if (gateNumber > 0)
			{
                IncreaseCarNumber();
			}
			else if (gateNumber < 0)
			{
                DecreaseCarNumber();
			}
		}

		if (other.gameObject.CompareTag("Punch") && this.leftCars.Count >= 7 || this.righttCars.Count >= 7)
		{
            Punch();
		}

		if (other.gameObject.CompareTag("Saw") && this.leftCars.Count >= 7 || this.righttCars.Count >= 7)
		{
			Saw();
		}

		if (other.gameObject.CompareTag("Finish"))
		{
            blueTruck.GetComponent<Movement>().moveSpeed = 0;
            redTruck.GetComponent<Movement>().moveSpeed = 0;
            EqualCars();
		}


	}

    private void IncreaseCarNumber()
	{
        
		if (this.gameObject.tag == "BlueTruck")
		{
			
            for (int i = 0; i < gateNumber; i++)
            {
                GameObject newCar = Instantiate(carPrefab);
                newCar.transform.SetParent(transform);
                newCar.transform.position = new Vector3(leftCarsTransform[leftindexOfTransforms].transform.position.x, leftCarsTransform[leftindexOfTransforms].transform.position.y, leftCarsTransform[leftindexOfTransforms].transform.position.z);
                #region Car Scale Animation
                mySeq = DOTween.Sequence();
                mySeq.Append(newCar.transform.DOScale(1.3f, 0.3f));
                mySeq.Append(newCar.transform.DOScale(1, 0.3f));
                #endregion
                leftCars.Add(newCar);
                leftindexOfTransforms += 1;
            }
        }
		else if (this.gameObject.tag == "RedTruck")
		{
			for (int i = 0; i < gateNumber; i++)
			{
                GameObject newCar = Instantiate(carPrefab);
                newCar.transform.SetParent(transform);
                newCar.transform.position = new Vector3(rightCarsTransform[rightindexOfTransforms].transform.position.x, rightCarsTransform[rightindexOfTransforms].transform.position.y, rightCarsTransform[rightindexOfTransforms].transform.position.z);
                #region Car Scale Animation
                mySeq = DOTween.Sequence();
                mySeq.Append(newCar.transform.DOScale(1.3f, 0.3f));
                mySeq.Append(newCar.transform.DOScale(1, 0.3f));
                #endregion
                righttCars.Add(newCar);
                rightindexOfTransforms += 1;
            }
        }
      
	}

    private void DecreaseCarNumber()
	{
		if (this.gameObject.tag == "BlueTruck")
		{
			if (leftTargetCount >= 0 )
			{
                for (int i = leftCars.Count - 1; i >= leftTargetCount; i--)
                {
                    #region Car Dec Animation
                    float moveRandomX = Random.Range(-20, 20);
                    mySeq = DOTween.Sequence();
                    mySeq.Append(leftCars[i].transform.DOMoveZ(-2, 2));
                    mySeq.Join(leftCars[i].transform.DOMoveX(moveRandomX, 2));
                    mySeq.Join(leftCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                    #endregion
                    Destroy(leftCars[i], 2);
                    leftCars.RemoveAt(i);
                    leftindexOfTransforms -= 1;
                }
            }
			else if (leftTargetCount < 0)
			{
				for (int i = leftCars.Count -1; i >= 0; i--)
				{
                    #region Car Dec Animation
                    float moveRandomX = Random.Range(-20, 20);
                    mySeq = DOTween.Sequence();
                    mySeq.Append(leftCars[i].transform.DOMoveZ(-2, 2));
                    mySeq.Join(leftCars[i].transform.DOMoveX(moveRandomX, 2));
                    mySeq.Join(leftCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                    #endregion
                    Destroy(leftCars[i], 2);
                    leftCars.RemoveAt(i);
                    leftindexOfTransforms = 0;
                }
			}
            
        }

		else if (this.gameObject.tag == "RedTruck")
		{
			if (rightTargetCount >= 0)
			{
                for (int i = righttCars.Count - 1; i >= rightTargetCount; i--)
                {
                    #region Car Dec Animation
                    float moveRandomX = Random.Range(-20, 20);
                    mySeq = DOTween.Sequence();
                    mySeq.Append(righttCars[i].transform.DOMoveZ(-2, 2));
                    mySeq.Join(righttCars[i].transform.DOMoveX(moveRandomX, 2));
                    mySeq.Join(righttCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                    #endregion
                    Destroy(righttCars[i], 2);
                    righttCars.RemoveAt(i);
                    rightindexOfTransforms -= 1;
                }
            }

			else if (rightTargetCount < 0)
			{
                for (int i = righttCars.Count - 1; i >= 0; i--)
                {
                    #region Car Dec Animation
                    float moveRandomX = Random.Range(-20, 20);
                    mySeq = DOTween.Sequence();
                    mySeq.Append(righttCars[i].transform.DOMoveZ(-2, 2));
                    mySeq.Join(righttCars[i].transform.DOMoveX(moveRandomX, 2));
                    mySeq.Join(righttCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                    #endregion
                    Destroy(righttCars[i], 2);
                    righttCars.RemoveAt(i);
                    rightindexOfTransforms = 0;
                }
            }
			
		}
        
		
	}

    private void SwapCars()
	{
		if (MobileInput.Instance.SwipeLeft)
		{
            StartCoroutine(ShuffleDelay());
		}

		else if (MobileInput.Instance.SwipeRight)
		{
            StartCoroutine(ShuffleDelayLeft());
        } 
	}

    IEnumerator ShuffleDelay()
	{
        int blueTruckListCount = blueTruck.GetComponent<Movement>().leftCars.Count;
        int rightLastIndex = -1;
        int blueTrukPlacementIndex = -1;
        for (int i = 0; i < righttCars.Count / 4 ; i++)
        {
            
            righttCars[righttCars.Count + rightLastIndex].transform.SetParent(blueTruck.transform);
            #region Car Shuffle Animation
            mySeq = DOTween.Sequence();
            mySeq.Append(righttCars[righttCars.Count + rightLastIndex].transform.DOMoveX(-00.1f, 0.2f));
            mySeq.Join(righttCars[righttCars.Count + rightLastIndex].transform.DOMoveY(15, 0.2f));
            mySeq.Append(righttCars[righttCars.Count + rightLastIndex].transform.DOMove(new Vector3(leftCarsTransform[blueTruckListCount + blueTrukPlacementIndex].transform.position.x, leftCarsTransform[blueTruckListCount + blueTrukPlacementIndex].transform.position.y, leftCarsTransform[blueTruckListCount + blueTrukPlacementIndex].transform.position.z + 15), 0.5f));
            #endregion
            blueTruck.GetComponent<Movement>().leftCars.Add(righttCars[righttCars.Count + rightLastIndex]);
            righttCars.RemoveAt(righttCars.Count + rightLastIndex);
            rightLastIndex--;
            blueTrukPlacementIndex++;
            leftindexOfTransforms++;
            rightindexOfTransforms--;
			yield return new WaitForSeconds(0.1f);


        }
    }

    IEnumerator ShuffleDelayLeft()
	{
        int redTruckListCount = redTruck.GetComponent<Movement>().righttCars.Count;
        int leftLastIndex = -1;
        int redTruckPlacementIndex = -1;
		for (int i = 0; i < leftCars.Count /4; i++)
		{
            leftCars[leftCars.Count + leftLastIndex].transform.SetParent(redTruck.transform);
            #region Car Shuffle Animation
            mySeq = DOTween.Sequence();
            mySeq.Append(leftCars[leftCars.Count + leftLastIndex].transform.DOMoveX(-00.1f, 0.2f));
            mySeq.Join(leftCars[leftCars.Count + leftLastIndex].transform.DOMoveY(15, 0.2f));
            mySeq.Append(leftCars[leftCars.Count + leftLastIndex].transform.DOMove(new Vector3(rightCarsTransform[redTruckListCount + redTruckPlacementIndex].transform.position.x, rightCarsTransform[redTruckListCount + redTruckPlacementIndex].transform.position.y, rightCarsTransform[redTruckListCount + redTruckPlacementIndex].transform.position.z + 15), 0.5f));
            #endregion
            redTruck.GetComponent<Movement>().righttCars.Add(leftCars[leftCars.Count + leftLastIndex]);
            leftCars.RemoveAt(leftCars.Count + leftLastIndex);
            leftLastIndex--;
            redTruckPlacementIndex++;
            rightindexOfTransforms++;
            leftindexOfTransforms--;
            yield return new WaitForSeconds(0.1f);
		}
	}

    
    private void Punch()
	{
        int leftTruckAfterPunch = leftCars.Count - 3;
        int rightTruckAfterPunch = righttCars.Count - 3;
		if (this.gameObject.tag == "BlueTruck")
		{
			for (int i = leftCars.Count - 1; i >= leftTruckAfterPunch; i--)
			{
                #region Car Dec Animation
                float moveRandomX = Random.Range(-20, 20);
                mySeq = DOTween.Sequence();
                mySeq.Append(leftCars[i].transform.DOMoveZ(-2, 2));
                mySeq.Join(leftCars[i].transform.DOMoveX(moveRandomX, 2));
                mySeq.Join(leftCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                #endregion
                Destroy(leftCars[i], 2);
                leftCars.RemoveAt(i);
                leftindexOfTransforms -= 1;
            }
        }

		else if (this.gameObject.tag == "RedTruck")
		{
            for (int i = righttCars.Count - 1; i >= rightTruckAfterPunch; i--)
            {
                #region Car Dec Animation
                float moveRandomX = Random.Range(-20, 20);
                mySeq = DOTween.Sequence();
                mySeq.Append(leftCars[i].transform.DOMoveZ(-2, 2));
                mySeq.Join(leftCars[i].transform.DOMoveX(moveRandomX, 2));
                mySeq.Join(leftCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                #endregion
                Destroy(righttCars[i], 2);
                righttCars.RemoveAt(i);
                rightindexOfTransforms -= 1;
            }
        }
	}

    private void Saw()
    {
        int leftTruckAfterSaw = leftCars.Count - 4;
        int rigtTruckAfterSaw = righttCars.Count - 4;
        if (this.gameObject.tag == "BlueTruck")
        {
            for (int i = leftCars.Count - 1; i >= leftTruckAfterSaw; i--)
            {
                #region Car Dec Animation
                float moveRandomX = Random.Range(-20, 20);
                mySeq = DOTween.Sequence();
                mySeq.Append(leftCars[i].transform.DOMoveZ(-2, 2));
                mySeq.Join(leftCars[i].transform.DOMoveX(moveRandomX, 2));
                mySeq.Join(leftCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                #endregion
                Destroy(leftCars[i], 2);
                leftCars.RemoveAt(i);
                leftindexOfTransforms -= 1;
            }
        }

		if (this.gameObject.tag == "RedTruck")
		{
            for (int i = righttCars.Count - 1; i >= rigtTruckAfterSaw; i--)
            {
                #region Car Dec Animation
                float moveRandomX = Random.Range(-20, 20);
                mySeq = DOTween.Sequence();
                mySeq.Append(leftCars[i].transform.DOMoveZ(-2, 2));
                mySeq.Join(leftCars[i].transform.DOMoveX(moveRandomX, 2));
                mySeq.Join(leftCars[i].transform.DORotate(new Vector3(-180, 0, 0), 0.5f));
                #endregion
                Destroy(righttCars[i], 2);
                righttCars.RemoveAt(i);
                rightindexOfTransforms -= 1;
            }
        }
    }

    private void EqualCars()
	{
        int totalCar = redTruck.GetComponent<Movement>().righttCars.Count + blueTruck.GetComponent<Movement>().leftCars.Count;
        
		if (redTruck.GetComponent<Movement>().righttCars.Count > blueTruck.GetComponent<Movement>().leftCars.Count)
		{
			for (int i = 0; i < totalCar/2 - blueTruck.GetComponent<Movement>().leftCars.Count; i++)
			{
                StartCoroutine(ShuffleDelay());
			}
		}
		else if (blueTruck.GetComponent<Movement>().leftCars.Count > redTruck.GetComponent<Movement>().righttCars.Count)
		{
            for (int i = 0; i < totalCar / 2 - redTruck.GetComponent<Movement>().righttCars.Count; i++)
            {
                StartCoroutine(ShuffleDelayLeft());
            }
        }
	}

   

}
