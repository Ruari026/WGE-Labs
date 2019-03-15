using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDropSpawner : MonoBehaviour
{ 
    public GameObject dropBlockPrefab;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            int b = Random.Range(1, 5);
            Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
            pos.y += 10;
            SpawnDropBlock(b, pos);
        }
    }

    // When game object is enabled
    void OnEnable()
    {
        VoxelChunk.OnEventBlockDestroyed += SpawnDropBlock;
    }
    // When game object is disabled
    void OnDisable()
    {
        VoxelChunk.OnEventBlockDestroyed -= SpawnDropBlock;
    }

    void SpawnDropBlock(int blockType, Vector3 blockPosition)
    {
        GameObject newDroppedBlock = Instantiate(dropBlockPrefab, this.transform);
        newDroppedBlock.transform.position = new Vector3(blockPosition.x + 0.5f, blockPosition.y + 0.5f, blockPosition.z + 0.5f);

        newDroppedBlock.GetComponent<VoxelBlock>().CreateBlock(blockType);
    }
}
