package medicalPersonnel;

import MedicalSpecialization.MedicalSpecialization4Doc;

import java.util.List;

public class Doctor {

    Doctor() {
    }

    public static String firstName;
    public static String lastName;
    private static MedicalSpecialization4Doc specialization4Doc;
    private List<Patient> patients;
    private List<Assistant> assistants;

    public static String getFirstName() {
        return firstName;
    }

    public static void setFirstName(String firstName) {
        Doctor.firstName = firstName;
    }

    public static String getLastName() {
        return lastName;
    }

    public static void setLastName(String lastName) {
        Doctor.lastName = lastName;
    }

    public static MedicalSpecialization4Doc getSpecialization4Doc() {
        return specialization4Doc;
    }

    public static void setSpecialization4Doc(MedicalSpecialization4Doc specialization4Doc) {
        Doctor.specialization4Doc = specialization4Doc;
    }

    public List<Patient> getPatients() {
        return patients;
    }

    public void setPatients(List<Patient> patients) {
        this.patients = patients;
    }

    public List<Assistant> getAssistants() {
        return assistants;
    }

    public void setAssistants(List<Assistant> assistants) {
        this.assistants = assistants;
    }
}
