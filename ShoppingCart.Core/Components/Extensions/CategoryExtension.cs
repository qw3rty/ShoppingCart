using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Core.Dtos.Responses;
using ShoppingCart.Core.Services.Discounts.Implementations.DiscountRules;
using ShoppingCart.Core.Services.Discounts.Interfaces.DiscountRules;
using ShoppingCart.Domain.Campaigns;
using ShoppingCart.Domain.Categories;

namespace ShoppingCart.Core.Components.Extensions
{
    public static class CategoryExtension
    {
        public static CategoryDto ToDto(this Category entity)
        {
            if (entity == null)
                return null;

            return new CategoryDto
            {
                Title = entity.Title,
                Parent = entity.Parent.ToDto(),
                CampaignList = entity.CampaignList.Select(x => x.ToDto()).ToList()
            };
        }

        public static Category ToEntity(this CategoryDto dto)
        {
            if (dto == null)
                return null;

            return new Category(dto.Title, dto.Parent.ToEntity(), dto.CampaignList.Select(x => x.ToEntity()).ToList());
        }


        public static CampaignDto ToDto(this Campaign dto)
        {
            if (dto == null)
                return null;
            switch (dto.CampaignRule.CampaignRuleType)
            {
                case CampaignRuleType.Amount:
                    return new CampaignDto(
                        new AmountCampaignDiscountRule(
                            ((IAmountCampaignRuleConditions) dto.CampaignRule.RuleConditions).Price,
                            ((IAmountCampaignRuleConditions) dto.CampaignRule.RuleConditions).Quantity
                        ));

                case CampaignRuleType.Rate:
                    return new CampaignDto(
                        new RateCampaignDiscountRule(
                            ((IRateCampaignRuleConditions) dto.CampaignRule.RuleConditions).Percentage,
                            ((IRateCampaignRuleConditions) dto.CampaignRule.RuleConditions).Quantity
                        ));
                default:
                    return null;
            }
        }
        public static Campaign ToEntity(this CampaignDto dto)
        {
            if (dto == null)
                return null;
            var name = dto.CampaignDiscountRule.GetType().Name;
            switch (name)
            {
                case nameof(AmountCampaignDiscountRule):
                    return new Campaign
                    {
                        CampaignRule = new CampaignRule
                        {
                            CampaignRuleType = CampaignRuleType.Amount,
                            RuleConditions = new AmountCampaignRuleConditions
                            (
                                ((IAmountCampaignDiscountRule) dto.CampaignDiscountRule).Price,
                                ((IAmountCampaignDiscountRule) dto.CampaignDiscountRule).Quantity
                            )
                        }
                    };
                case nameof(RateCampaignDiscountRule):
                    return new Campaign
                    {
                        CampaignRule = new CampaignRule
                        {
                            CampaignRuleType = CampaignRuleType.Rate,
                            RuleConditions = new RateCampaignRuleConditions
                            (
                                ((IRateCampaignDiscountRule) dto.CampaignDiscountRule).Percentage,
                                ((IRateCampaignDiscountRule) dto.CampaignDiscountRule).Quantity
                            )
                        }
                    };
            }

            return null;
        }
    }
}