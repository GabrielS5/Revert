package medicalPersonnel;

public class MedicalPersonnelFactory {
    public MedicalPersonnel getPersonnel(String personnelType) {
        if (personnelType == null)
            return null;
        if (personnelType.equalsIgnoreCase("Doctor"))
            return new Doctor();
        if (personnelType.equalsIgnoreCase("Assistant"))
            return new Assistant();
        return null;
    }

}
