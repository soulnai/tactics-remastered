using UnityEngine;
using System.Collections;
using EnumSpace;
using UnityEngine.UI;

public class AttributeUISliderController : UIWatchElement
{
    public unitAttributes Attribute;
    private Slider slider;
    private float maxVal;

    public override void StartWatch()
    {
        BaseAttribute attributeToWatch = UnitToWatch.AttributesList.Find(x => x.attribute == Attribute);
        slider = GetComponent<Slider>();
        updateSlider(attributeToWatch, 0f, attributeToWatch.Value);
        GM.Events.OnUnitAttributeChange += updateSlider;
    }

    private void updateSlider(BaseAttribute attribute, float prevVal, float currVal)
    {
        if ((attribute.attribute == Attribute)&&(UnitToWatch.AttributesList.Contains(attribute)))
        {
            maxVal = currVal;
            switch (attribute.attribute)
            {
                case unitAttributes.AP:
                    maxVal = UnitToWatch.APMax.Value;
                    break;
                case unitAttributes.HP:
                    maxVal = UnitToWatch.HPMax.Value;
                    break;
                case unitAttributes.MP:
                    maxVal = UnitToWatch.MPMax.Value;
                    break;
            }
            slider.normalizedValue = currVal/maxVal;
        }
    }
}
