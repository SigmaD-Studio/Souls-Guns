using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vachamdiahinh : MonoBehaviour
{
    // Start is called before the first frame update
    void handlePlayerCollision(Player& player, Terrain& terrain)
    {
        Rectangle playerBox = player.getBoundingBox();
        Rectangle terrainBox = terrain.getBoundingBox();
        if (checkCollision(playerBox, terrainBox))
        {

            Vector2f collisionDepth = calculateCollisionDepth(playerBox, terrainBox);
            player.setPosition(player.getPosition() + collisionDepth);
            player.updateBoundingBox();
        }
    }
    bool checkCollision(const Rectangle& rect1, const Rectangle& rect2) {
        return rect1.intersects(rect2);
        }
Vector2f calculateCollisionDepth(const Rectangle& rect1, const Rectangle& rect2)
{
    
    float overlapX = min(rect1.getRight() - rect2.getLeft(), rect2.getRight() - rect1.getLeft());
    float overlapY = min(rect1.getBottom() - rect2.getTop(), rect2.getBottom() - rect1.getTop());

  
    float collisionDepthX = (abs(overlapX) < abs(overlapY)) ? overlapX : 0.0f;
    float collisionDepthY = (abs(overlapY) < abs(overlapX)) ? overlapY : 0.0f;

   
    return Vector2f(collisionDepthX, collisionDepthY);
}
