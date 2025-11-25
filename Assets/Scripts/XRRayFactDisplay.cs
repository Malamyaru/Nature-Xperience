using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using UnityEngine.UI;

public class XRRayFactDisplay : MonoBehaviour
{
    [Header("XR Ray Interactors")]
    public XRRayInteractor leftRayInteractor;
    public XRRayInteractor rightRayInteractor;

    [Header("English UI References")]
    public TMP_Text factTextEN;
    public Image factImageEN;

    [Header("Japanese UI References")]
    public TMP_Text factTextJP;
    public Image factImageJP;

    void Update()
    {
        if (!factTextEN || !factImageEN || !factTextJP || !factImageJP)
            return;

        bool hitFound =
            CheckRayHit(leftRayInteractor) ||
            CheckRayHit(rightRayInteractor);

        factTextEN.gameObject.SetActive(hitFound);
        factImageEN.gameObject.SetActive(hitFound);
        factTextJP.gameObject.SetActive(hitFound);
        factImageJP.gameObject.SetActive(hitFound);

        if (!hitFound)
        {
            factTextEN.text = "";
            factTextJP.text = "";
        }
    }

    private bool CheckRayHit(XRRayInteractor interactor)
    {
        if (interactor == null)
            return false;

        if (interactor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            switch (hit.collider.tag)
            {
                // ---------------------- BEETLE ----------------------
                case "Body":
                    factTextEN.text = "Kabutomushi can lift up to 850 times their own body weight.";
                    factTextJP.text = "カブトムシは自分の体重の850倍まで持ち上げることができます。";
                    return true;

                case "Horn":
                    factTextEN.text = "Male beetles use their horns to flip and fight rivals during mating season.";
                    factTextJP.text = "オスのカブトムシは、交尾期に角を使ってライバルをひっくり返して戦います。";
                    return true;

                case "Leg":
                    factTextEN.text = "Their strong legs help them cling to trees and dig into soil for protection.";
                    factTextJP.text = "強力な脚は木にしがみついたり、土に潜って身を守るのに役立ちます。";
                    return true;

                // ---------------------- LARVAE ----------------------
                case "Larvaebody":
                    factTextEN.text = "Larvae have soft, white bodies, making them vulnerable to predators.";
                    factTextJP.text = "幼虫の体は柔らかく白いため、捕食者に狙われやすいです。";
                    return true;

                case "Larvaehead":
                    factTextEN.text = "Larvae feed only on decaying plant matter, such as rotting wood.";
                    factTextJP.text = "幼虫は腐った木などの植物の腐敗物しか食べません。";
                    return true;

                // ---------------------- PUPAE ----------------------
                case "Pupaebody":
                    factTextEN.text = "Inside the pupa, the larva completely transforms before emerging as an adult beetle.";
                    factTextJP.text = "さなぎの中で、幼虫は完全に変態し成虫のカブトムシになります。";
                    return true;

                case "Pupaehorn":
                    factTextEN.text = "The beetle's horn begins forming during the pupal stage and hardens before emerging.";
                    factTextJP.text = "角はさなぎの段階で形成され始め、成虫になる前に硬化します。";
                    return true;

                // ---------------------- FROG ----------------------
                case "Frogbody":
                    factTextEN.text = "Japanese tree frogs can survive extreme cold, even temperatures as low as -30°C.";
                    factTextJP.text = "ニホンアマガエルは氷点下30度の極寒環境でも生き延びることができます。";
                    return true;

                case "Froghead":
                    factTextEN.text = "Their large eyes give them a wide field of vision to spot prey and predators.";
                    factTextJP.text = "大きな目により、獲物や捕食者を見つけやすくなっています。";
                    return true;

                case "Frogleg":
                    factTextEN.text = "Their powerful hind legs let them jump many times their own body length.";
                    factTextJP.text = "強力な後ろ足で、自分の体の数倍の距離を跳ぶことができます。";
                    return true;

                // ---------------------- CRAB ----------------------
                case "Crabbody":
                    factTextEN.text = "Sawagani have a hard carapace that protects them in rocky stream habitats.";
                    factTextJP.text = "サワガニは岩場の川で生きるために硬い甲羅で身を守っています。";
                    return true;

                case "Crabclaw":
                    factTextEN.text = "They use their claws for defense and to pick up insects and plants for food.";
                    factTextJP.text = "ハサミを使って身を守り、小さな昆虫や植物をつまんで食べます。";
                    return true;

                case "Crabeye":
                    factTextEN.text = "Their eyes move independently, helping them watch for danger in all directions.";
                    factTextJP.text = "目を別々に動かし、あらゆる方向の危険を見張ることができます。";
                    return true;

                // ---------------------- OIKAWA  ----------------------
                case "Fishbody":
                    factTextEN.text = "Oikawa have 7–10 reddish spots along their sides. Males turn bright red and blue-green during breeding season.";
                    factTextJP.text = "オイカワの体側には7〜10個の赤い斑点があり、繁殖期の雄は赤や青緑に鮮やかに変わります。";
                    return true;

                case "Fishhead":
                    factTextEN.text = "Male Oikawa grow white bumps called 'nuptial tubercles' on their head during breeding season.";
                    factTextJP.text = "繁殖期になると、雄のオイカワの頭には“追星”と呼ばれる白い突起が現れます。";
                    return true;

                case "Fishtail":
                    factTextEN.text = "Their forked tail lets them make sharp turns when chasing insects in fast-flowing rivers.";
                    factTextJP.text = "二又の尾びれで川の速い流れの中でも素早く方向転換し、虫を追いかけます。";
                    return true;

                default:
                    return false;
            }
        }
        return false;
    }
}
