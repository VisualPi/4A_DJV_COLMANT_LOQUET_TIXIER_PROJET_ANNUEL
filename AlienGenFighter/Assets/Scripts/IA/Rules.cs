using System.Collections.Generic;
using System;

public class RuleCondition
{
	public Func<SquareContext, bool> _func;
	public RuleCondition(Func<SquareContext, bool> func)
	{
		_func = func;
	}
	public bool Test(SquareContext c)
	{
		return _func(c);
	}
}
public class RuleAction
{
	public Action<SquareContext> _action;

	public RuleAction(Action<SquareContext> action)
	{
		_action = action;
	}
	public void Execute(SquareContext context)
	{
        _action(context);
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