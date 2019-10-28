package medicalPersonnel;

import MedicalSpecialization.MedicalSpecialization4Doc;
import patient.PatientImpl;

import java.util.List;

public class Doctor implements MedicalPersonnel {

    Doctor() {
    }

    public static String firstName;
    public static String lastName;
    private static MedicalSpecialization4Doc specialization4Doc;
    private List<PatientImpl> patients;
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

    public List<PatientImpl> getPatients() {
        return patients;
    }

    public void setPatients(List<PatientImpl> patients) {
        this.patients = patients;
    }

    public List<Assistant> getAssistants() {
        return assistants;
    }

    public void setAssistants(List<Assistant> assistants) {
        this.assistants = assistants;
    }

    @Override
    public void readMedicalFile() {

    }

    @Override
    public String consultAndGiveDiagnostic() {
        return null;
    }

    @Override
    public void writeInMedicalFile() {

    }

    @Override
    public void giveTreatment() {

    }
}
