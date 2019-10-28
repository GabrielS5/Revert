package medicalPersonnel;

import MedicalSpecialization.MedicalSpec4Assistant;

import java.util.List;

public class Assistant {

    public static String firstName;
    public static String lastName;
    private static MedicalSpec4Assistant spec4Assistant;
    private List<Patient> patients;
    private List<Doctor> doctors;

    public static String getFirstName() {
        return firstName;
    }

    public static void setFirstName(String firstName) {
        Assistant.firstName = firstName;
    }

    public static String getLastName() {
        return lastName;
    }

    public static void setLastName(String lastName) {
        Assistant.lastName = lastName;
    }

    public static MedicalSpec4Assistant getSpec4Assistant() {
        return spec4Assistant;
    }

    public static void setSpec4Assistant(MedicalSpec4Assistant spec4Assistant) {
        Assistant.spec4Assistant = spec4Assistant;
    }

    public List<Patient> getPatients() {
        return patients;
    }

    public void setPatients(List<Patient> patients) {
        this.patients = patients;
    }

    public List<Doctor> getDoctors() {
        return doctors;
    }

    public void setDoctors(List<Doctor> doctors) {
        this.doctors = doctors;
    }
}
