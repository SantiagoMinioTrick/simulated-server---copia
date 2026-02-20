using SimulatedBE.Networking.DTOs;

public interface IModifier
{
    CardDto ApplyModifier(CardDto cardToApply);

    bool CanUseModifier(CardDto cardOwner, InstructionDto mainInstruction);

    InstructionDto UseModifier(CardDto cardOwner, InstructionDto mainInstruction);
}
