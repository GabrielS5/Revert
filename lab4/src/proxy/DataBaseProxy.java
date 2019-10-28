package proxy;

public class DataBaseProxy implements DataBaseProxyInterface {

    private DataBaseService dataBaseService;

    @Override
    public void setDataBaseService(DataBaseService dataBaseService) {
        this.dataBaseService = dataBaseService;
    }

    @Override
    public void addRecord(Record record, User user) {

    }

    @Override
    public void addUser(User user) {

    }

    @Override
    public void addRecord(Record record) {

    }

    @Override
    public void addDraft(Draft draft) {

    }

    @Override
    public Record getRecord() {
        return null;
    }

    @Override
    public User getUser() {
        return null;
    }

    @Override
    public Draft getDraft() {
        return null;
    }


}
