package patient;

public class PatientProxy implements Patient {

    private static Patient patient;

    @Override
    public String receiveTreatment() {
        if (patient == null)
            patient = new PatientImpl();
        return patient.receiveTreatment();
    }
}
