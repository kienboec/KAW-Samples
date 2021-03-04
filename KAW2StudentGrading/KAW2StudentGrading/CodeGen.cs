


// Component: StudentComponent
                
namespace KAW2StudentGrading.StudentComponent {

    public class Student {
        public string Name { get; set; }
    }

}


                
namespace KAW2StudentGrading.StudentComponent {

    public class PC {
        public string Name { get; set; }
    }

}


                
// Component: LectureComponent
                
namespace KAW2StudentGrading.LectureComponent {

    public class Lecture {
        public string Name { get; set; }
    }

}


                
namespace KAW2StudentGrading.LectureComponent {

    public class LectureGrade {
        public string Name { get; set; }
    }

}


                

/* 
C:\Users\Daniel Kienbock\modelio\workspace\KAW2StudentGrading\XMI\kaw2studentgrading.xmi
_________________________________________________________
<?xml version="1.0" encoding="UTF-8"?>
<xmi:XMI xmi:version="2.1" xmlns:xmi="http://schema.omg.org/spec/XMI/2.1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:JavaExtensions="http:///schemas/JavaExtensions/_7AcyAHztEeu-z8v0Tq_Cyg/0" xmlns:ecore="http://www.eclipse.org/emf/2002/Ecore" xmlns:uml="http://www.eclipse.org/uml2/3.0.0/UML" xsi:schemaLocation="http:///schemas/JavaExtensions/_7AcyAHztEeu-z8v0Tq_Cyg/0 JavaExtensions.profile.xmi#_7AoYNXztEeu-z8v0Tq_Cyg">
  <uml:Model xmi:id="_6_ZCEHztEeu-z8v0Tq_Cyg" name="kaw2studentgrading">
    <eAnnotations xmi:id="_6_ZCEXztEeu-z8v0Tq_Cyg" source="Objing">
      <contents xmi:type="uml:Property" xmi:id="_6_ZCEnztEeu-z8v0Tq_Cyg" name="exporterVersion">
        <defaultValue xmi:type="uml:LiteralString" xmi:id="_6_ZCE3ztEeu-z8v0Tq_Cyg" value="3.0.0"/>
      </contents>
    </eAnnotations>
    <packagedElement xmi:type="uml:Package" xmi:id="_6_ZCFHztEeu-z8v0Tq_Cyg" name="StudentComponent">
      <packagedElement xmi:type="uml:Class" xmi:id="_6_ZCFXztEeu-z8v0Tq_Cyg" name="Student"/>
      <packagedElement xmi:type="uml:Association" xmi:id="_6_ZCFnztEeu-z8v0Tq_Cyg" name="visits" memberEnd="_6_ZCF3ztEeu-z8v0Tq_Cyg _6_ZCGnztEeu-z8v0Tq_Cyg">
        <ownedEnd xmi:id="_6_ZCF3ztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZpKHztEeu-z8v0Tq_Cyg" association="_6_ZCFnztEeu-z8v0Tq_Cyg">
          <upperValue xmi:type="uml:LiteralUnlimitedNatural" xmi:id="_6_ZCGHztEeu-z8v0Tq_Cyg" value="*"/>
          <lowerValue xmi:type="uml:LiteralInteger" xmi:id="_6_ZCGXztEeu-z8v0Tq_Cyg"/>
        </ownedEnd>
        <ownedEnd xmi:id="_6_ZCGnztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZCFXztEeu-z8v0Tq_Cyg" association="_6_ZCFnztEeu-z8v0Tq_Cyg">
          <upperValue xmi:type="uml:LiteralUnlimitedNatural" xmi:id="_6_ZCG3ztEeu-z8v0Tq_Cyg" value="*"/>
          <lowerValue xmi:type="uml:LiteralInteger" xmi:id="_6_ZCHHztEeu-z8v0Tq_Cyg"/>
        </ownedEnd>
      </packagedElement>
      <packagedElement xmi:type="uml:Association" xmi:id="_6_ZCHXztEeu-z8v0Tq_Cyg" name="own" memberEnd="_6_ZCHnztEeu-z8v0Tq_Cyg _6_ZCIXztEeu-z8v0Tq_Cyg">
        <ownedEnd xmi:id="_6_ZCHnztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZpIHztEeu-z8v0Tq_Cyg" association="_6_ZCHXztEeu-z8v0Tq_Cyg">
          <upperValue xmi:type="uml:LiteralUnlimitedNatural" xmi:id="_6_ZCH3ztEeu-z8v0Tq_Cyg" value="*"/>
          <lowerValue xmi:type="uml:LiteralInteger" xmi:id="_6_ZCIHztEeu-z8v0Tq_Cyg"/>
        </ownedEnd>
        <ownedEnd xmi:id="_6_ZCIXztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZCFXztEeu-z8v0Tq_Cyg" association="_6_ZCHXztEeu-z8v0Tq_Cyg">
          <lowerValue xmi:type="uml:LiteralInteger" xmi:id="_6_ZCInztEeu-z8v0Tq_Cyg"/>
        </ownedEnd>
      </packagedElement>
      <packagedElement xmi:type="uml:Class" xmi:id="_6_ZpIHztEeu-z8v0Tq_Cyg" name="PC">
        <ownedAttribute xmi:id="_6_ZpIXztEeu-z8v0Tq_Cyg" name="CountOfCores" visibility="public" isUnique="false">
          <type xmi:type="uml:PrimitiveType" href="pathmap://UML_LIBRARIES/UMLPrimitiveTypes.library.uml#String"/>
        </ownedAttribute>
        <ownedOperation xmi:id="_6_ZpInztEeu-z8v0Tq_Cyg" name="DoWork" visibility="public"/>
      </packagedElement>
      <packagedElement xmi:type="uml:Association" xmi:id="_6_ZpI3ztEeu-z8v0Tq_Cyg" name="relates to" memberEnd="_6_ZpKnztEeu-z8v0Tq_Cyg _6_ZpJHztEeu-z8v0Tq_Cyg">
        <ownedEnd xmi:id="_6_ZpJHztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZpKXztEeu-z8v0Tq_Cyg" association="_6_ZpI3ztEeu-z8v0Tq_Cyg">
          <upperValue xmi:type="uml:LiteralUnlimitedNatural" xmi:id="_6_ZpJXztEeu-z8v0Tq_Cyg" value="*"/>
          <lowerValue xmi:type="uml:LiteralInteger" xmi:id="_6_ZpJnztEeu-z8v0Tq_Cyg"/>
        </ownedEnd>
      </packagedElement>
    </packagedElement>
    <packagedElement xmi:type="uml:Package" xmi:id="_6_ZpJ3ztEeu-z8v0Tq_Cyg" name="LectureComponent">
      <packagedElement xmi:type="uml:Class" xmi:id="_6_ZpKHztEeu-z8v0Tq_Cyg" name="Lecture"/>
      <packagedElement xmi:type="uml:Class" xmi:id="_6_ZpKXztEeu-z8v0Tq_Cyg" name="LectureGrade">
        <ownedAttribute xmi:id="_6_ZpKnztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZCFXztEeu-z8v0Tq_Cyg" association="_6_ZpI3ztEeu-z8v0Tq_Cyg"/>
        <ownedAttribute xmi:id="_6_ZpK3ztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZpKHztEeu-z8v0Tq_Cyg" association="_6_ZpLHztEeu-z8v0Tq_Cyg"/>
      </packagedElement>
      <packagedElement xmi:type="uml:Association" xmi:id="_6_ZpLHztEeu-z8v0Tq_Cyg" name="is graded by" memberEnd="_6_ZpK3ztEeu-z8v0Tq_Cyg _6_ZpLXztEeu-z8v0Tq_Cyg">
        <ownedEnd xmi:id="_6_ZpLXztEeu-z8v0Tq_Cyg" visibility="public" type="_6_ZpKXztEeu-z8v0Tq_Cyg" association="_6_ZpLHztEeu-z8v0Tq_Cyg">
          <upperValue xmi:type="uml:LiteralUnlimitedNatural" xmi:id="_6_ZpLnztEeu-z8v0Tq_Cyg" value="*"/>
          <lowerValue xmi:type="uml:LiteralInteger" xmi:id="_6_ZpL3ztEeu-z8v0Tq_Cyg"/>
        </ownedEnd>
      </packagedElement>
    </packagedElement>
    <profileApplication xmi:id="_7CKCMHztEeu-z8v0Tq_Cyg">
      <eAnnotations xmi:id="_7CKpQHztEeu-z8v0Tq_Cyg" source="http://www.eclipse.org/uml2/2.0.0/UML">
        <references xmi:type="ecore:EPackage" href="JavaExtensions.profile.xmi#_7AoYNXztEeu-z8v0Tq_Cyg"/>
      </eAnnotations>
      <appliedProfile href="JavaExtensions.profile.xmi#_7AnxIHztEeu-z8v0Tq_Cyg"/>
    </profileApplication>
  </uml:Model>
  <JavaExtensions:JavaPackage xmi:id="_7CMecHztEeu-z8v0Tq_Cyg" base_Package="_6_ZCEHztEeu-z8v0Tq_Cyg"/>
  <JavaExtensions:JavaPackage xmi:id="_7CNFgHztEeu-z8v0Tq_Cyg" base_Package="_6_ZCFHztEeu-z8v0Tq_Cyg"/>
  <JavaExtensions:JavaClass xmi:id="_7CNskHztEeu-z8v0Tq_Cyg" base_Class="_6_ZCFXztEeu-z8v0Tq_Cyg"/>
  <JavaExtensions:JavaClass xmi:id="_7CNskXztEeu-z8v0Tq_Cyg" base_Class="_6_ZpIHztEeu-z8v0Tq_Cyg"/>
  <JavaExtensions:JavaPackage xmi:id="_7COToHztEeu-z8v0Tq_Cyg" base_Package="_6_ZpJ3ztEeu-z8v0Tq_Cyg"/>
  <JavaExtensions:JavaClass xmi:id="_7CO6sHztEeu-z8v0Tq_Cyg" base_Class="_6_ZpKHztEeu-z8v0Tq_Cyg"/>
  <JavaExtensions:JavaClass xmi:id="_7CO6sXztEeu-z8v0Tq_Cyg" base_Class="_6_ZpKXztEeu-z8v0Tq_Cyg"/>
</xmi:XMI>

*/