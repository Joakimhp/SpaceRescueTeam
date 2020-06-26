using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteUtility
{
    static Vector2 topCenterOfTargetSprite;

    static Vector2 distancefromPivotToCenter;
    static Vector2 bottomCenterOfSprite;


    public static Vector2 VerticalSpriteSnapToTop( SpriteRenderer sprite , SpriteRenderer targetSprite ) {
        topCenterOfTargetSprite = targetSprite.transform.position.GetXYVector2 () + new Vector2 ( targetSprite.sprite.bounds.center.x , targetSprite.sprite.bounds.center.y + targetSprite.sprite.bounds.extents.y ) * targetSprite.transform.localScale;
        //topCenterOfTargetSprite = new Vector2 ( targetSprite.transform.position.x , targetSprite.transform.position.y + targetSprite.sprite.bounds.extents.y * targetSprite.transform.localScale.y );
        topCenterOfTargetSprite = new Vector2 ( topCenterOfTargetSprite.x , targetSprite.sprite.bounds.min.y + targetSprite.transform.position.y );
        Debug.Log ( "tCOTS: " + targetSprite.sprite.bounds.center.y );
        //distancefromPivotToCenter = ( sprite.sprite.pivot * sprite.transform.localScale ) - ( sprite.sprite.bounds.center.GetXYVector2 () * sprite.transform.localScale.GetXYVector2 () );
        //bottomCenterOfSprite = new Vector2 ( sprite.sprite.bounds.center.x , sprite.sprite.bounds.center.y - sprite.sprite.bounds.extents.y * sprite.transform.localScale.y );

        //Vector2 newPosition = topCenterOfTargetSprite + bottomCenterOfSprite + distancefromPivotToCenter;

        //return newPosition;
        return Vector3.zero;
    }

    private static Vector2 CalculateDistanceFromPivotToCenter( SpriteRenderer renderer ) {
        Vector2 centerOfSpriteInPixels = renderer.sprite.rect.center;
        Debug.Log ( renderer.sprite.rect );
        Vector2 sum = renderer.sprite.pivot / renderer.sprite.bounds.size;
        return sum - centerOfSpriteInPixels;

        //return renderer.sprite.pivot 
        //return ( renderer.sprite.pivot / new Vector2 ( renderer.sprite.rect.width , renderer.sprite.rect.height ) );
    }
}
