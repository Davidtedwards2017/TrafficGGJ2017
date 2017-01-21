class AssignMaterial extends ScriptableWizard
{
    var theMaterial : Material;
   
    function OnWizardUpdate()
    {
        helpString = "Select Game Obects";
        isValid = (theMaterial != null);
    }
   
    function OnWizardCreate()
    {
       
        var gameObjects = Selection.gameObjects;
   
        for (var gameObject in gameObjects)
        {
            gameObject.GetComponent.<Renderer>().material = theMaterial;
        }
    }
   
    @MenuItem("Custom/Assign Material", false, 3)
    static function assignMaterial()
    {
        ScriptableWizard.DisplayWizard(
            "Assign Material", AssignMaterial, "Assign");
    }
}