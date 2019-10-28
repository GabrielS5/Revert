package patient;

import medicalPersonnel.Assistant;
import medicalPersonnel.Doctor;
import patologies.Disease;

import java.util.List;

public class PatientImpl implements Patient {

    public static String firstName;
    public static String lastName;
    private Doctor doctor;
    private Assistant assistant;
    private List<Disease> diseases;

    public static String getFirstName() {
        return firstName;
    }

    public static void setFirstName(String firstName) {
        PatientImpl.firstName = firstName;
    }

    public static String getLastName() {
        return lastName;
    }

    public static void setLastName(String lastName) {
        PatientImpl.lastName = lastName;
    }

    public Doctor getDoctor() {
        return doctor;
    }

    public void setDoctor(Doctor doctor) {
        this.doctor = doctor;
    }

    public Assistant getAssistant() {
        return assistant;
    }

    public void setAssistant(Assistant assistant) {
        this.assistant = assistant;
    }

    public List<Disease> getDiseases() {
        return diseases;
    }

    public void setDiseases(List<Disease> diseases) {
        this.diseases = diseases;
    }

    @Override
    public String receiveTreatment() {
        return null;
    }
}
