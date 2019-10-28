package strategy;

import proxy.Record;

public class RecordClassifier {

    private RecordProcessorInterface recordProcessorInterface;


    public RecordClassifier(RecordProcessorInterface recordProcessorInterface){
        this.recordProcessorInterface = recordProcessorInterface;
    }


    public String classify(Record record){
        //todo algorithm
        return recordProcessorInterface.runFor(record);
        //todo algorithm
    }


}
