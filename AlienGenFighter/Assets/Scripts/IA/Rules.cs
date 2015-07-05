using System.Collections.Generic;
using System;

public class RuleCondition
{
    public Func<EntityScript, bool> _func;

    public RuleCondition(Func<EntityScript, bool> func)
    {
        _func = func;
    }

    public bool Test(EntityScript e)
    {
        return _func(e);
    }
}

public class RuleAction
{
    public Action<EntityScript> _action;

    public RuleAction(Action<EntityScript> action)
    {
        _action = action;
    }

    public void Execute(EntityScript entity)
    {
        _action(entity);
    }
}

public class Rules
{
    public RuleCondition Condition;
    public RuleAction Action;
    public Rules(RuleCondition condition, RuleAction action)
    {
        Condition = condition;
        Action = action;
    }
}

public class RulesGroup
{
    private List<Rules> _ruleList = new List<Rules>();
    public List<Rules> RuleList {
        get { return _ruleList; }
    }
}