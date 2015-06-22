using System.Collections.Generic;
using System;
using Assets.Scripts.Context;

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
    public RuleCondition _condition;
    public RuleAction _action;
    public Rules(RuleCondition condition, RuleAction action)
    {
        _condition = condition;
        _action = action;
    }
}
public class RulesGroup
{
    private List<Rules> _ruleList = new List<Rules>();
    public List<Rules> GetRuleList()
    {
        return _ruleList;
    }
}