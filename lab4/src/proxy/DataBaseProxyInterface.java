package proxy;

public interface DataBaseProxyInterface {

    public void setDataBaseService(DataBaseService dataBaseService);

    public void addRecord(Record record,User user);

    public void addUser(User user);

    public void addRecord(Record record);

    public void addDraft(Draft draft);

    public Record getRecord();

    public User getUser();

    public Draft getDraft();



}
