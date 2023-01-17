using System;
using System.Collections.Generic;

public static class Conditionals
{
    public class CaseConditional<T>
    {
        public CaseConditional(T value) => Value = value;

        public T Value { get; }

        public CaseWhenConditional<T> When(T condition) => new CaseWhenConditional<T>(this, condition);
        public Dictionary<T, CaseWhenConditional<T>> Conditions = new Dictionary<T, CaseWhenConditional<T>>();
        public CaseElseConditional<T> CaseElseConditional;
        public void RegisterWhen(CaseWhenConditional<T> caseWhenConditional) => Conditions.Add(caseWhenConditional.Condition, caseWhenConditional);
        public void RegisterElse(CaseElseConditional<T> caseElseConditional) => CaseElseConditional = caseElseConditional;
        public void Execute()
        {
            if (Conditions.TryGetValue(Value, out var whenCondition))
            {
                whenCondition.Execute();
            }
        }
    }

    public class CaseWhenConditional<T>
    {
        public CaseConditional<T> CaseConditional;
        public T Condition;

        public CaseWhenConditional(CaseConditional<T> caseConditional, T condition)
        {
            Condition = condition;
            CaseConditional = caseConditional;
            CaseConditional.RegisterWhen(this);
        }

        public CaseWhenThenConditional<T> Then(Action action) => new CaseWhenThenConditional<T>(this, action);
        private CaseWhenThenConditional<T> Consequence;
        public void RegisterConsequence(CaseWhenThenConditional<T> consequence) => Consequence = consequence;

        public CaseWhenThenConditional<T> Then(IEndConditional innerConditional) => new CaseWhenThenConditional<T>(this, innerConditional);

        internal void Execute() => Consequence.Execute();
    }

    public class CaseWhenThenConditional<T>
    {
        private CaseWhenConditional<T> CaseWhenConditional;
        private Action action;
        private IEndConditional InnerConditional;

        private CaseWhenThenConditional(CaseWhenConditional<T> caseWhenConditional)
        {
            CaseWhenConditional = caseWhenConditional;
            CaseWhenConditional.RegisterConsequence(this);
        }

        public CaseWhenThenConditional(CaseWhenConditional<T> caseWhenConditional, Action action) : this(caseWhenConditional) => this.action = action;

        public CaseWhenThenConditional(CaseWhenConditional<T> caseWhenConditional, IEndConditional innerConditional) => InnerConditional = innerConditional;
        
        public CaseWhenConditional<T> When(T condition) => new CaseWhenConditional<T>(CaseWhenConditional.CaseConditional, condition);
        public CaseElseConditional<T> Else(Action action) => new CaseElseConditional<T>(CaseWhenConditional.CaseConditional, action);
        public CaseEndConditional<T> End() => new CaseEndConditional<T>(CaseWhenConditional.CaseConditional);

        internal void Execute()
        {
            action?.Invoke();
            InnerConditional?.Execute();
        }
    }

    public class CaseElseConditional<T> : IEndConditional
    {
        private CaseConditional<T> CaseConditional;
        private Action action;

        public CaseElseConditional(CaseConditional<T> caseConditional, Action action)
        {
            CaseConditional = caseConditional;
            this.action = action;
            CaseConditional.RegisterElse(this);
        }

        public void Execute() => CaseConditional.Execute();
    }

    public class CaseEndConditional<T> : IEndConditional
    {
        private CaseConditional<T> caseConditional;

        public CaseEndConditional(CaseConditional<T> caseConditional)
        {
            this.caseConditional = caseConditional;
        }

        public void Execute() => caseConditional.Execute();
    }

    public interface IEndConditional
    {
        void Execute();
    }
    
    public static CaseConditional<T> Case<T>(T value) => new CaseConditional<T>(value);

    public static void Do(IEndConditional conditional) => conditional.Execute();
}
