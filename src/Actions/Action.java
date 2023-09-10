import java.util.function.BiFunction;
import java.util.function.BiConsumer;

public abstract class ActionData {
    private Type returnType;
    private Type inLeftType;
    private Type inRightType;
    protected String description;

    public ActionData(Type returnTypeIn, Type inLeftTypeIn, Type inRightTypeIn) {
        this.returnType = returnTypeIn;
        this.inLeftType = inLeftTypeIn;
        this.inRightType = inRightTypeIn;

        if (returnType == null || inLeftType == null || inRightType == null) {
            throw new RuntimeException("ActionData created with null type");
        }
    }

    public String toString() {
        return description;
    }

    public String getTypesString() {
        return returnType.getString() + " <- " + inLeftType.getString() + "." + inRightType.getString();
    }

    protected void setDescription(String desc) {
        this.description = desc;
    }
}

public class VoidAction extends ActionData {
    public VoidAction() {
        super(VoidType.INSTANCE, VoidType.INSTANCE, VoidType.INSTANCE); // Assuming VoidType is a singleton
        setDescription("Void Action");
    }

    public Object execute(Object inLeft, Object inRight) {
        return null;
    }

    public void addToProg(Action inLeft, Action inRight, JavProgram prog) {
        if (prog.getExprLevel() > 0) {
            prog.comment("void");
        }
    }

    public String getDescription() {
        return StringUtil.putStringInTreeNodeBox("void"); // Assuming str is a utility class named StringUtil
    }
}

public class LambdaAction extends ActionData {
    private BiFunction<Object, Object, Object> lambda;
    private BiConsumer<Action, Action, JavProgram> JavWriter;

    public LambdaAction(Type inLeftTypeIn, Type inRightTypeIn, Type returnTypeIn,
                        BiFunction<Object, Object, Object> lambdaIn,
                        BiConsumer<Action, Action, JavProgram> JavWriterIn,
                        String textIn) {
        super(returnTypeIn, inLeftTypeIn, inRightTypeIn);

        if (JavWriterIn == null) {
            this.JavWriter = (inLeft, inRight, prog) -> {
                prog.comment("lambda action '" + textIn + "' has not yet been implemented for C++");
            };
        } else {
            this.JavWriter = JavWriterIn;
        }

        if (lambdaIn == null) {
            this.lambda = (inLeft, inRight) -> {
                throw new RuntimeException("action '" + textIn + "' has not yet been implemented for the interpreter");
            };
        } else {
            this.lambda = lambdaIn;
        }

        setDescription(textIn);
    }

    public Object execute(Object inLeft, Object inRight) {
        return lambda.apply(inLeft, inRight);
    }

    public void addToProg(Action inLeft, Action inRight, JavProgram prog) {
        JavWriter.accept(inLeft, inRight, prog);
    }

    public String getDescription() {
        return StringUtil.putStringInTreeNodeBox(description);
    }
}

// Assuming Action is a class that wraps ActionData
public static Action lambdaAction(Type inLeftTypeIn, Type inRightTypeIn, Type returnTypeIn,
                                  BiFunction<Object, Object, Object> lambdaIn,
                                  BiConsumer<Action, Action, JavProgram> JavWriter,
                                  String textIn) {
    return new Action(new LambdaAction(inLeftTypeIn, inRightTypeIn, returnTypeIn, lambdaIn, JavWriter, textIn));
}

public static Action createNewVoidAction() {
    return new Action(new VoidAction());
}
